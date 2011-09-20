var labelType, useGradients, nativeTextSupport, animate;

(function () {
    var ua = navigator.userAgent,
      iStuff = ua.match(/iPhone/i) || ua.match(/iPad/i),
      typeOfCanvas = typeof HTMLCanvasElement,
      nativeCanvasSupport = (typeOfCanvas == 'object' || typeOfCanvas == 'function'),
      textSupport = nativeCanvasSupport
        && (typeof document.createElement('canvas').getContext('2d').fillText == 'function');
    //I'm setting this based on the fact that ExCanvas provides text support for IE
    //and that as of today iPhone/iPad current text support is lame
    labelType = (!nativeCanvasSupport || (textSupport && !iStuff)) ? 'Native' : 'HTML';
    nativeTextSupport = labelType == 'Native';
    useGradients = nativeCanvasSupport;
    animate = !(iStuff || !nativeCanvasSupport);
})();



function init(constituentId) {
    var jsonData = "";

    $("#nodeTemplate").template("nodeTemplate");
    //init Spacetree
    //Create a new ST instance
    var st = new $jit.ST({
        //id of viz container element
        injectInto: 'infovis',
        //set duration for the animation
        duration: 800,
        //set animation transition type
        transition: $jit.Trans.Quart.easeInOut,
        //set distance between node and its children
        levelDistance: 50,
        orientation: 'top',
        //enable panning
        Navigation: {
            enable: true,
            panning: true
        },
        //set node and edge styles
        //set overridable=true for styling individual
        //nodes or edges
        Node: {
            height: 40,
            width: 120,
            type: 'stroke-rect',
            color: '#aaa',
            overridable: true
        },

        Edge: {
            type: 'bezier',
            overridable: true
        },

        onBeforeCompute: function (node) {
            // Console.write("loading " + node.name);
        },

        onAfterCompute: function () {
            //Console.write("done");
        },

        //This method is called on DOM label creation.
        //Use this method to add event handlers and styles to
        //your node.


        onCreateLabel: function (label, node) {
            label.id = node.id;
            //label.innerHTML = node.name;


            var nodeData = { id: node.id, name: node.name, spouseName: node.data.spouse,
                memberUrl: node.data.familyMemberUrl, memberId: "const_" + node.data.familyMemberId,
                spouseUrl: node.data.spouseUrl, spouseId: "const_" + node.spouseId, marriagedate: node.data.marriageDate, parents: node.data.spouseParents
            };

            $("#nodeTemplate").tmpl(nodeData).appendTo(label);
            var tooltip = $("#nodePopupTemplate").tmpl(nodeData);

            $(".popup-member", tooltip).click(function () {
                window.open(node.data.memberUrl);
                return false
            });

            $(".popup-spouse", tooltip).click(function () {
                window.open(node.data.spouseUrl);
                return false
            });
            $(label).poshytip({
                className: "tip-yellow",
                bgImageFrameSize: 10,
                alignTo: "target",
                alignX: "center",
                offsetY: 5,
                showTimeout: 500,
                hideTimeout: 250,
                fade: false,
                slide: false,
                showOn: "hover",
                allowTipHover: true,
                content: tooltip
            });

            label.onclick = function () {

                var constId = node.data.familyMemberId;
                if (constId > 0) {
                    $.getJSON('http://localhost/kallivayalilService/KallivayalilService.svc/Relationships?constituentId=' + constId, function (data) {
                        jsonData = data;
                        //load json data
                        st.loadJSON(jsonData);
                        //compute node positions and layout
                        st.compute();
                        //optional: make a translation of the tree
                        st.geom.translate(new $jit.Complex(0, 0), "current");
                        //emulate a click on the root node.
                        st.onClick(st.root, {
                            Move: {
                                offsetY: 90
                            }
                        });
                        //end
                    });
                }

            };
            //set label styles
            var style = label.style;
            style.width = 120 + 'px';
            style.height = 37 + 'px';
            style.cursor = 'pointer';
            style.color = '#333';
            style.fontSize = '0.9em';
            style.textAlign = 'center';
            style.paddingTop = '3px';
        },

        //This method is called right before plotting
        //a node. It's useful for changing an individual node
        //style properties before plotting it.
        //The data properties prefixed with a dollar
        //sign will override the global node style properties.
        onBeforePlotNode: function (node) {
            //add some color to the nodes in the path between the
            //root node and the selected node.
            if (node.data.familyMemberId == constituentId) {
                node.data.$color = "#ff7";
            }
            else {
                delete node.data.$color;
                //if the node belongs to the last plotted level
                if (!node.anySubnode("exist")) {
                    //count children number
                    var count = 0;
                    node.eachSubnode(function (n) { count++; });
                    //assign a node color based on
                    //how many children it has
                    node.data.$color = ['#aaa', '#baa', '#caa', '#daa', '#eaa', '#faa'][count];
                }
            }
        },

        //This method is called right before plotting
        //an edge. It's useful for changing an individual edge
        //style properties before plotting it.
        //Edge data proprties prefixed with a dollar sign will
        //override the Edge global style properties.
        onBeforePlotLine: function (adj) {
            if (adj.nodeFrom.selected && adj.nodeTo.selected) {
                adj.data.$color = "#eed";
                adj.data.$lineWidth = 3;
            }
            else {
                delete adj.data.$color;
                delete adj.data.$lineWidth;
            }
        }
    });

    $jit.ST.Plot.NodeTypes.implement({
        "stroke-rect": {
            render: function (p, o) {
                var width = p.getData("width"),
                    height = p.getData("height"),
                    align = p.getData("align"),
                    alignedPos = this.getAlignedPos(p.pos.getc(true), width, height, align),
                    linewidth = p.getData("lineWidth"),
                    z = st.canvas.getCtx();
                z.save();
                z.lineWidth = linewidth;
                var originX = alignedPos.x,
                    originY = alignedPos.y,
                    calculatedX = alignedPos.x + width,
                    calculatedY = alignedPos.y + height,
                    m = p.getData("dim");
                var l = Math.PI / 180;
                if ((calculatedX - originX) - (2 * m) < 0) {
                    m = ((calculatedX - originX) / 2)
                }
                if ((calculatedY - originY) - (2 * m) < 0) {
                    m = ((calculatedY - originY) / 2)
                }
                z.beginPath();
                z.moveTo(originX + m, originY);
                z.lineTo(calculatedX - m, originY);
                z.arc(calculatedX - m, originY + m, m, l * 270, l * 360, false);
                z.lineTo(calculatedX, calculatedY - m);
                z.arc(calculatedX - m, calculatedY - m, m, l * 0, l * 90, false);
                z.lineTo(originX + m, calculatedY);
                z.arc(originX + m, calculatedY - m, m, l * 90, l * 180, false);
                z.lineTo(originX, originY + m);
                z.arc(originX + m, originY + m, m, l * 180, l * 270, false);
                z.stroke();
                z.restore()
            }
        }
    });

    $.getJSON('http://localhost/kallivayalilService/KallivayalilService.svc/Relationships?constituentId=' + constituentId, function (data) {
        jsonData = data;
        //load json data
        st.loadJSON(jsonData);
        //compute node positions and layout
        st.compute();
        //optional: make a translation of the tree
        st.geom.translate(new $jit.Complex(0, 0), "current");
        //emulate a click on the root node.
        st.onClick(st.root, {
            Move: {
                offsetY: 90
            }
        });
        //end
    });


}