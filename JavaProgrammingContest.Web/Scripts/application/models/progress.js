var ProgressModel;

$(window).ready(function() {
    /**
     * Progress for Assignment Model
     */
    ProgressModel = Backbone.Model.extend({
        // Default attributes
        defaults: function() {
            return {
                Id: 0,
                StartTime: 0
            };
        },
        
        idAttribute: 'Id',
        
        urlRoot: '/api/progress'
    });
});