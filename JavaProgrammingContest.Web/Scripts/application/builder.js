var BuilderFactory;

$(window).ready(function () {
    /**
     * Assignment Model
     */
    BuilderFactory = {

        build: function (code, complete) {
            this._call('build', JSON.stringify({ BuildJob: { Code: code } }), function (data) {
                complete(data);
            });
        },

        run: function(code, complete) {
            this._call('run', JSON.stringify({ BuildJob: { Code: code } }), function(data) {
                complete(data);
            });
        },

        _call: function(controller, data, success) {
            return $.ajax({
                url: '/api/' + controller,
                type: 'POST',
                dataType: 'json',
                cache: false,
                success: success,
                error: function (data, a, b) {
                    noty({ text: 'Something went wrong trying to build the program: ' + b, type: 'error' });
                },
                data: data
            });
        }
    };
});