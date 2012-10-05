function isEmail(string) {
    if (string.search(/^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/) != -1) {
        return true;
    }
    return false; 
}
function isPhone(string) {
    if (string.search(/\d{3}\-\d{3}\-\d{4}/)) {
        return true;
    }
    return false;
}