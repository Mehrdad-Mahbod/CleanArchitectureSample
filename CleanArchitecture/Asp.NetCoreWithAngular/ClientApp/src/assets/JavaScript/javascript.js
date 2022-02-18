window.addEventListener("resize", function () {
    if (window.innerWidth < 768) {
        document.getElementById("bs-canvas-right").style.width = 0;
        document.getElementById("MainRenderBody").classList.remove("ms-5");
        document.getElementsByClassName("UpFooter").item(0).classList.remove("ms-5");
    }
    else {
        document.getElementById("bs-canvas-right").style.width = '50px';
        document.getElementById("MainRenderBody").classList.add("ms-5");
        document.getElementsByClassName("UpFooter").item(0).classList.add("ms-5");
    }
});