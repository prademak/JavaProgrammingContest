var SolutionModel;

$(window).ready(function() {
    /**
     * Assignment Model
     */
    SolutionModel = Backbone.Model.extend({
        // Default attributes
        defaults: function() {
            return {
                Id: 0,

                Code: '',
                Time: 0.0
            };
        },
        
        idAttribute: 'Id',

        // Initialize the model
        initialize: function (arg) {
            console.log(arg);
        }
    });
});