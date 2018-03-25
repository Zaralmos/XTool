using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using XTool.Models.Roles;
using System.ComponentModel.DataAnnotations;
using XTool.Models.TransferModels;
using XTool.Models.Shared;
using XTool.Models.UserManager;

namespace XTool.UserManager
{
    public static class UserManagerExtensions
    {
        public static async Task<OperationResult> RegisterConfirmedUserAsync(this UserManager<XToolUser> userManager, UserRegisterModel model)
        {
            OperationResult result = null;
            result = await userManager.RegisterUserAsync(model);
            if (result.Status == Statuses.Ok)
            {
                XToolUser user = await userManager.FindByEmailAsync(model.Email);
                result = await userManager.ConfirmUserAsync(user);
                if (result.Status != Statuses.Ok)
                {
                    await userManager.DeleteAsync(user);
                    result = new OperationResult() { Status = Statuses.Ok, Message = "Произонла ошибка подтверждения пользователя!"};
                }
                else
                    result = new OperationResult() { Status = Statuses.Ok, Message = "Пользователь успешно зарегистрирован и его аккаунт подтверждён.", Data = user.Id};
            }
            return result;
        }

        public static async Task<OperationResult> RegisterUserAsync(this UserManager<XToolUser> userManager, UserRegisterModel model)
        {
            Statuses status = Statuses.Error;
            string message = null;
            OperationResult auxres;
            if (string.IsNullOrWhiteSpace(model.Email) || string.IsNullOrWhiteSpace(model.Password)) // настроить человеческую валидацию модели 
                message = "Введены некорректные данные!";
            else if (model.Password != model.PasswordRepeat)
                message = "Пароли не совпадают!";
            else if (await userManager.FindByEmailAsync(model.Email) != null)
                message = "Пользователем с таким Email уже зарегистрирован в системе!";
            else
            {
                var newUser = new XToolUser() { Email = model.Email, FullName = model.Name, UserName = model.Email };
                var suc = await userManager.CreateAsync(newUser);
                if (!suc.Succeeded)
                    message = "Произошла ошибка при создании аккаунта!";
                else if ((auxres = await userManager.UpdatePasswordAsync(newUser, model.Password)).Status != Statuses.Ok)
                    message = auxres.Message;
                else
                {

                    IdentityResult result = null;
                    try
                    {
                        result = await userManager.AddToRoleAsync(newUser, model.RoleName);
                    }
                    catch
                    {
                    }
                    if (result?.Succeeded ?? false)
                    {
                        message = $"Аккаунт {newUser.Email} успешно зарегистрирован. В ближайшее время администратор подтвердит ваш аккаунт и вы сможете войти в систему.";
                        status = Statuses.Ok;
                    }
                    else
                    {
                        await userManager.DeleteAsync(newUser);
                        message = "Не удалось настроить роль пользователя!";
                    }
                }
            }
            return new OperationResult() { Status = status, Message = message };
        }

        public static async Task<OperationResult> UpdatePasswordAsync(this UserManager<XToolUser> userManager, XToolUser user, string newPassword)
        {
            OperationResult result;
            // настроить валидацию пароля
            if (newPassword.Length < 6 || newPassword.Length > 20 || newPassword.ToUpper() == newPassword)
                result = new OperationResult()
                {
                    Status = Statuses.Error,
                    Message = "Пароль должен содержать от 6 до 20 символов и содержать как строчные, так и заглавные буквы!"
                };
            else if (user.PasswordHash != userManager.PasswordHasher.HashPassword(user, newPassword))
            {
                user.PasswordHash = userManager.PasswordHasher.HashPassword(user, newPassword);
                await userManager.UpdateAsync(user);
                result = new OperationResult() { Status = Statuses.Ok, Message = "Пароль успешно изменён." };
            }
            else
                result = new OperationResult() { Status = Statuses.AlreadyDone, Message = "Новый и текущий пароли совпадают" };
            return result;
        }

        public static async Task<OperationResult> ConfirmUserAsync(this UserManager<XToolUser> userManager, XToolUser user)
        {
            OperationResult result;
            if (!user.IsConfirmed)
            {
                user.IsConfirmed = true;
                await userManager.UpdateAsync(user);
                result = new OperationResult() { Status = Statuses.Ok, Message = "Пользователь успешно подтверждён." };
            }
            else
                result = new OperationResult() { Status = Statuses.AlreadyDone, Message = "Новый и текущий пароли совпадают" };
            return result;
        }

        public static async Task<OperationResult> BanUserAsync(this UserManager<XToolUser> userManager, XToolUser user)
        {
            OperationResult result;
            if (!user.IsBanned)
            {
                user.IsBanned = true;
                await userManager.UpdateAsync(user);
                result = new OperationResult() { Status = Statuses.Ok, Message = "Пользователь успешно забанен." };
            }
            else
                result = new OperationResult() { Status = Statuses.AlreadyDone, Message = "Пользователь был забанен ранее." };
            return result;
        }

        public static async Task<OperationResult> UnbanUserAsync(this UserManager<XToolUser> userManager, XToolUser user)
        {
            OperationResult result;
            if (user.IsBanned)
            {
                user.IsBanned = false;
                await userManager.UpdateAsync(user);
                result = new OperationResult() { Status = Statuses.Ok, Message = "Пользователь успешно разбанен." };
            }
            else
                result = new OperationResult() { Status = Statuses.AlreadyDone, Message = "Не удалось разбанить пользователя, так как он не был забанен." };
            return result;
        }
    }
}
