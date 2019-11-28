function set_cookies(name, value) {
    var d = new Date();
    d.setTime(d.getTime() + 3600 * 1000 * 24 * 365); //1 hour
    document.cookie = name + "=" + escape(value) + "; expires=" + d.toUTCString() + "; path=/";
}

function set_cookie(name, value, expires, path, domain, secure) {
    document.cookie = name + "=" + escape(value) +
        ((expires) ? "; expires=" + expires : "") +
        ((path) ? "; path=" + path : "") +
        ((domain) ? "; domain=" + domain : "") +
        ((secure) ? "; secure" : "");
}

function set_cookie_short(name, value, expires) {
    if (!expires) {
        expires = new Date();
    }
    document.cookie = name + "=" + escape(value) + "; expires=" + expires.toGMTString() + "; path=/";
}

// устанавливает cookie с именем name и значением value
// options - объект с свойствами cookie (expires, path, domain, secure)
function setcookie(name, value, options) {
    options = options || {};

    var expires = options.expires;

    if (typeof expires == "number" && expires) {
        var d = new Date();
        d.setTime(d.getTime() + expires * 1000);
        expires = options.expires = d;
    }
    if (expires && expires.toUTCString) {
        options.expires = expires.toUTCString();
    }

    value = encodeURIComponent(value);

    var updatedCookie = name + "=" + value;

    for (var propName in options) {
        updatedCookie += "; " + propName;
        var propValue = options[propName];
        if (propValue !== true) {
            updatedCookie += "=" + propValue;
        }
    }
    document.cookie = updatedCookie;
}

function get_cookie(name) {
    var cookie = " " + document.cookie;
    var search = " " + name + "=";
    var setStr = null;
    var offset = 0;
    var end = 0;
    if (cookie.length > 0) {
        offset = cookie.indexOf(search);
        if (offset != -1) {
            offset += search.length;
            end = cookie.indexOf(";", offset)
            if (end == -1) {
                end = cookie.length;
            }
            setStr = unescape(cookie.substring(offset, end));
        }
    }
    return (setStr);
}

function get_cookie_a(name) {
    cookie_name = name + "=";
    cookie_length = document.cookie.length;
    cookie_begin = 0;
    while (cookie_begin < cookie_length) {
        value_begin = cookie_begin + cookie_name.length;
        if (document.cookie.substring(cookie_begin, value_begin) == cookie_name) {
            var value_end = document.cookie.indexOf(";", value_begin);
            if (value_end == -1) {
                value_end = cookie_length;
            }
            return unescape(document.cookie.substring(value_begin, value_end));
        }
        cookie_begin = document.cookie.indexOf(" ", cookie_begin) + 1;
        if (cookie_begin == 0) {
            break;
        }
    }
    return null;
}

function getcookie(name) {
    var matches = document.cookie.match(new RegExp(
        "(?:^|; )" + name.replace(/([\.$?*|{}\(\)\[\]\\\/\+^])/g, '\\$1') + "=([^;]*)"
    ));
    return matches ? decodeURIComponent(matches[1]) : undefined;
}

function del_cookie(name) {
    document.cookie = name + "=" + "; expires=Thu, 01 Jan 1970 00:00:01 GMT";
}

function delete_cookie(name) {
    setcookie(name, "", {
        expires: -1
    })
}

function set_prodview(view) {
    if (view != null) {
        if (!navigator.cookieEnabled) { alert('Включите cookie для комфортной работы с сайтом'); }

        var d = new Date();
        d.setTime(d.getTime() + 3600 * 1000 * 24 * 365); //1 year
        set_cookie("prodview", view, d.toUTCString(), "/")

        location.reload();
    }
}

function set_prodsortby(sort) {
    if (sort != null) {
        if (!navigator.cookieEnabled) { alert('Включите cookie для комфортной работы с сайтом'); }

        var d = new Date();
        d.setTime(d.getTime() + 3600 * 1000 * 24 * 365); //1 year
        set_cookie("prodsortby", sort, d.toUTCString(), "/")
        location.reload();
    }
}

function get_prodview() {
    return get_cookie("prodview")
}

function set_nrecbypage(npsize) {
    if (npsize != null) {
        if (!navigator.cookieEnabled) { alert('Включите cookie для комфортной работы с сайтом'); }

        var d = new Date();
        d.setTime(d.getTime() + 3600 * 1000 * 24 * 365); //1 year
        set_cookie("npsize", npsize, d.toUTCString(), "/")
        location.reload();
    }
}