﻿var BuilderFactory;

$(window).ready(function() {
    /**
     * Assignment Model
     */
    BuilderFactory = {
        build: function(code, complete) {
            this._call('build', JSON.stringify({ Code: code }), function(data) {
                complete(data);
            });
        },

        run: function(code, complete) {
            this._call('run', JSON.stringify({ Code: code }), function(data) {
                complete(data);
            });
        },
        submit: function (code, complete) {
            this._call('scores', JSON.stringify({ Code: code, Id: 1 }), function (data) {
                complete(data);
            });
        },

        _call: function(controller, data, success) {
            return $.ajax({
                url: '/api/' + controller,
                type: 'POST',
                dataType: 'json',
                contentType: 'application/json',
                cache: false,
                success: success,
                error: function(data, a, b) {
                    noty({ text: 'Something went wrong trying to build the program: ' + b, type: 'error' });
                },
                data: data
            });
        }
    };
});