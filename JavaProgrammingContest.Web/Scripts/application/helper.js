var APIHelper = {
    get: function (controller, data, success, error) {
        return this._call(controller, 'GET', data, success, error);
    },
    
    post: function (controller, data, success, error) {
        return this._call(controller, 'POST', data, success, error);
    },

    put: function (controller, data, success, error) {
        return this._call(controller, 'PUT', data, success, error);
    },
    
    delete: function(controller, data, success, error) {
        return this._call(controller, 'DELETE', data, success, error);
    },
    
    _call: function(controller, method, data, success, error){
        return $.ajax({
            url: '/api/' + controller,
            type: method.toUpperCase(),
            dataType: 'json',
            contentType: 'application/json',
            //cache: false,
            success: success,
            error: error,
            data: data
        });
    }
};