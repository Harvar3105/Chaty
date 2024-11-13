window.changeTheme = (elementId, newClass) => {
    var element = document.getElementById(elementId);
    if (element) {
        element.className = newClass;
    }
};