$(function () {

    var canvas = document.getElementById("intro-canvas");
    var ctx = canvas.getContext("2d");
    var canvasOffset = $("#intro-canvas").offset();
    var offsetX = canvasOffset.left;
    var offsetY = canvasOffset.top;
    var canvasWidth = canvas.width;
    var canvasHeight = canvas.height;
    var isDragging = false;

    var img = new Image();
    img.onload = function () {
        ctx.drawImage(img, canvas.width - 120, 100, 120, 120);
    };
    img.src = "/Content/Images/cookie.gif";
    img.onclick = function (e) { };

    var img2 = new Image();
    img2.onload = function () {
        ctx.drawImage(img2, 0, 0, );
    };
    img2.src = "/Content/Images/shauli.gif";



    ctx.font = "30px Comic Sans MS";
    ctx.fillStyle = "black";
    ctx.textAlign = "center";
    ctx.fillText("Have questions??? Ask shauli!", canvas.width - 400, 50);


    function handleMouseDown(e) {
        canMouseX = parseInt(e.clientX - offsetX);
        canMouseY = parseInt(e.clientY - offsetY);

        isDragging = true;
    }

    function handleMouseUp(e) {
        canMouseX = parseInt(e.clientX - offsetX);
        canMouseY = parseInt(e.clientY - offsetY);

        isDragging = false;
    }

    function handleMouseOut(e) {
        canMouseX = parseInt(e.clientX - offsetX);
        canMouseY = parseInt(e.clientY - offsetY);

        isDragging = false;
    }

    function handleMouseMove(e) {
        canMouseX = parseInt(e.clientX - offsetX);
        canMouseY = parseInt(e.clientY - offsetY);

        if (isDragging) {
            ctx.clearRect(0, 0, canvasWidth, canvasHeight);

            ctx.fillText("Have questions??? Ask shauli!", canvas.width - 400, 50);
            ctx.drawImage(img2, 0, 0);
            if (canMouseX < 170 && canMouseY < 200) {
                ctx.drawImage(img, canvas.width - 120, 100, 120, 120);
            }
            else {
                ctx.drawImage(img, canMouseX - 120 / 2, canMouseY - 120 / 2, 120, 120);
            }

        }
    }

    $("#intro-canvas").mousedown(function (e) { handleMouseDown(e); });
    $("#intro-canvas").mousemove(function (e) { handleMouseMove(e); });
    $("#intro-canvas").mouseup(function (e) { handleMouseUp(e); });
    $("#intro-canvas").mouseout(function (e) { handleMouseOut(e); });

}); 