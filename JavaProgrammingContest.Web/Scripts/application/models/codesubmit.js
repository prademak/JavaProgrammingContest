var AssignmentModel, AssignmentCollection;

$(window).ready(function () {
    /**
     * Assignment Model
     */
    AssignmentModel = Backbone.Model.extend({
        urlRoot: '/api/score/',
        idAttribute: "Id",

        // Default attributes
        defaults: function () {
            return {
                Id: 0,
                Code: ''
            };
        }
    });
});