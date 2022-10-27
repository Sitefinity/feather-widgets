var Querystring = function (qs) { // optionally pass a querystring to parse
    this.params = {};
    this.keys = [];

    if (qs === null) qs = location.search.substring(1, location.search.length);
    if (qs.length === 0) return;

    // Turn <plus> back to <space>
    // See: http://www.w3.org/TR/REC-html40/interact/forms.html#h-17.13.4.1
    qs = qs.replace(/\+/g, ' ');
    var args = qs.split('&'); // parse out name/value pairs separated via &

    // split out each name=value pair
    for (var i = 0; i < args.length; i++) {
        var currentElement = args[i];
        var equalsIndex = currentElement.indexOf('=');
        var name = currentElement.slice(0, equalsIndex);
        var value = decodeURIComponent(currentElement.slice(equalsIndex + 1));

        this.params[name] = value;
        this.keys[i] = name;
    }
};

Querystring.prototype = {
    get: function (key, default_) {
        var value = this.params[key.toLowerCase()];
        return (value !== null) ? value : default_;
    },

    contains: function (key) {
        var value = this.params[key.toLowerCase()];
        return (value !== null);
    },

    set: function (key, value) {
        var lowerKey = key.toLowerCase();
        if (this.keys.indexOf(lowerKey) == -1) {
            this.keys.push(lowerKey);
        }
        this.params[lowerKey] = value;
    },

    remove: function (key) {
        var lowerKey = key.toLowerCase();
        var index = this.keys.indexOf(lowerKey);
        if (index > -1) {
            this.keys.splice(index, 1);
        }
    },

    toString: function (appendQuestionMark) {
        if (this.keys.length > 0) {
            var query = appendQuestionMark ? "?" : "";
            for (var i = 0; i < this.keys.length; i++) {
                if (i > 0)
                    query += "&";
                query += this.keys[i] + "=" + this.params[this.keys[i]];
            }
            return query;
        }
        return "";
    }
};