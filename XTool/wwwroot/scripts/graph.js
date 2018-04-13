var graphsElems = document.getElementsByClassName('graph');
var graphsArr = Array.from(graphsElems);

class Graph {

    constructor(graph) {
        this.graph = graph;
        this.initGraph();
    }

    initGraph() {
        this.initConstants();
        this.initClassValues();
        this.redrawLine = () => {
            var points = "";
            this.vertexes.forEach(vertex => {
                points += (vertex.parentNode.offsetLeft + vertex.offsetLeft + vertex.offsetWidth / 2) + ',' + (vertex.offsetTop + vertex.offsetHeight / 2) + ' ';
            });
            this.line.setAttribute("points", points);
        }
        this.initEvents();
    }

    initConstants() {
        this.vertexValueClass = "vertex-value";
        this.graphVertexClass = "graph-vertex";
        this.lineClass = "polyline";
        this.graphEditableClass = "graph-editable";
        this.selectedVertexClass = "selected-vertex";
        this.preSelectedVertexClass = "pre-selected-vertex";
    }

    initEvents() {
        this.vertexValues.forEach((value) => {
            value.onmousedown = (e) => {
                e.stopPropagation();
            }

            value.oninput = (e) => {
                let curretnValue = +($(value).val());
                curretnValue = Math.min(curretnValue, 120);
                curretnValue = Math.max(curretnValue, 0);
                if (isNaN(curretnValue))
                    curretnValue = 0;
                $(value).val(curretnValue);
                let name = value.getAttribute('name');
                let vertex = this.vertexes.find(v => v.getAttribute('name') == name);
                vertex.style.top = ((120 - curretnValue) / 120 * 100) + '%';
                this.redrawLine();
            }
        });
        window.addEventListener("resize", this.redrawLine);
        this.graph.onresize += this.redrawLine();

        if (this.graph.classList.contains(this.graphEditableClass)) {
            this.vertexes.forEach((vertex) => {

                let vertexValue = vertex.getElementsByClassName(this.vertexValueClass)[0];

                let moveAt = (clientY) => {
                    vertexValue.value = 120 - (Math.max(Math.min(((clientY - vertex.offsetHeight / 2) - vertex.parentNode.getBoundingClientRect().top) / vertex.parentNode.offsetHeight, 1), 0) * 120).toFixed(0);
                    vertex.style.top = ((120 - vertexValue.value) / 120 * 100) + '%';
                    this.redrawLine();
                }

                vertex.onmousedown = (e) => { // 1. отследить нажатие

                    vertex.classList.add(this.selectedVertexClass);

                    document.onmousemove = (e) => {
                        moveAt(e.clientY);
                    }

                    document.onmouseup = () => {
                        vertex.classList.remove(this.selectedVertexClass)
                        document.onmousemove = null;
                        document.onmouseup = null;
                        this.redrawLine();
                    };

                    vertex.ondragstart = () => {
                        return false;
                    };

                    this.redrawLine();
                }

                vertex.addEventListener("touchstart", (e) => {
                    if (vertex.classList.contains(this.preSelectedVertexClass) === false) {

                        vertex.classList.add(this.preSelectedVertexClass)

                        let onVertexMoveStart = (e) => {

                            e.cancelable = true;
                            e.preventDefault();

                            vertex.classList.add(this.selectedVertexClass)

                            document.ontouchmove = (e) => {
                                moveAt(e.touches[0].clientY);
                            }

                            document.ontouchend = () => {
                                vertex.classList.remove(this.selectedVertexClass);
                                document.ontouchmove = null;
                                document.ontouchend = null;
                                this.redrawLine();
                            };

                            vertex.ondragstart = () => {
                                return false;
                            };
                        }

                        vertex.addEventListener("touchstart", onVertexMoveStart);

                        let onDocumentTouchStart = (e) => {
                            if (e.target != vertex) {
                                vertex.classList.remove(this.preSelectedVertexClass);
                                document.removeEventListener("touchstart", onDocumentTouchStart);
                                vertex.removeEventListener("touchstart", onVertexMoveStart);
                            }
                        }

                        document.addEventListener("touchstart", onDocumentTouchStart);
                    }
                });

            });
        }
    }

    initClassValues() {
        this.vertexValues = Array.from(this.graph.getElementsByClassName(this.vertexValueClass));
        this.vertexes = Array.from(this.graph.getElementsByClassName(this.graphVertexClass));
        this.line = this.graph.getElementsByClassName(this.lineClass)[0];
    }
}

var graphs = graphsArr.map(x => new Graph(x));
