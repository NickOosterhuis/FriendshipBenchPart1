function buildHeaders() {
    let headers = new Headers();
    let cookie = getCookie("JWT");
    headers.append("Authorization", "Bearer " + cookie);
    return headers;
}

function getCookie(name) {
    var value = "; " + document.cookie;
    var parts = value.split("; " + name + "=");
    if (parts.length == 2) return parts.pop().split(";").shift();
}