var CodeSubmitModel;

$(window).ready(function () {
    /**
     * Assignment Model
     */
    CodeSubmitModel = Backbone.Model.extend({
        urlRoot: '/api/scores/',
        idAttribute: "Id",

        // Default attributes
        defaults: function () {
            return {
                Id: 0,
                Code: ''
            };
        },
        
        save: function(attr, options) {
            return $.ajax($.extend({}, {
                //url: this.urlRoot+this.get('Id'),
                url: this.urlRoot,
                type: 'POST',
                dataType: 'json',
                contentType: 'application/json',
                cache: false,
                data: JSON.stringify(this.attributes)
            }, options));
        }
    });
});