$(".graph").on("Load", function (e) {
    let graph = $(e.target);

    //function GetData() {
    //    sendAjax({
    //        url: graph.attr("src"),
    //        data: {},
    //        success: function (result) {
    //            location.href = "/Home/Actor/" + result.relatedId;
    //        },
    //        error: function (result) {
    //            $("#form-create-actor .error").text(result.message);
    //        }
    //    })
    //}

    if (graph.attr("type").contains("Total")) {

    }
});

$(".graph-wrapper").each((i, graphWrapper) => {
    let graph = new Graph($(graphWrapper).children(".graph")[0]);
    let sets = $(graphWrapper).children(".graph-settings");

    function updateGraph(graph, data) {
        for (key in data) {
            let name = key[0].toUpperCase() + key.substr(1);
            let input = $("input[name=" + name + "]")
            input.val(data[key]);
            input.trigger('input');
            //let event = new Event('input', {
            //    'bubbles': true,
            //    'cancelable': true
            //});

            //input[0].dispatchEvent(event);
            //console.log(name + " " + $(graph.children('input[name=\"' + name + '\"]')).);
            //graph.children('input[name=\"' + name + '\"]').css("color", "red") // val(data[key]);
        }
        graph.redrawLine();
    }

    function LoadData() {
        let expertId = sets.children("select[name=ExpertId]").val();
        let isTotal = (expertId == 0 || isNaN(expertId));
        sendAjax({
            url: "/Scales/" + (isTotal ? "TotalScales" : "IndividualScales"), // sets.attr("src"),
            data: sets.serialize(),
            success: (res) => {
                updateGraph(graph, res.data);
                console.log(res.message);
            },
        });
    }

    //Object.keys(data).forEach((key) => alert(key));

    if (sets.length) {
        LoadData();
        setInterval(() => {
            LoadData();
        }, 7500);
        sets.children("input, select").on("click", (e) => LoadData())
    }
});
