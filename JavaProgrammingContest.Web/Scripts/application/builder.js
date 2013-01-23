var BuilderFactory;

$(window).ready(function () {
    /**
     * Assignment Model
     */
    BuilderFactory = {
        build: function (code, complete) {
            this._call('build', JSON.stringify({ Code: code }), function (data) {
                complete(data);
            });
        },

        run: function (code, complete) {
            this._call('run', JSON.stringify({ Code: code }), function (data) {
                complete(data);
            });
        },

        _call: function (controller, data, success) {
            return $.ajax({
                url: '/api/' + controller,
                type: 'POST',
                dataType: 'json',
                contentType: 'application/json',
                cache: false,
                success: success,
                error: function (data, a, b) {
                    noty({ text: 'Er ging iets fout tijdens het bouwen van de code: ' + b, type: 'error' });
                },
                data: data
            });
        }
    };
});