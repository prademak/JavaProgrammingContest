var AssignmentModel, AssignmentCollection;

$(window).ready(function () {
    /**
     * Assignment Model
     */
    AssignmentModel = Backbone.Model.extend({
        urlRoot: 'api/assignments/',
        idAttribute: "Id",

        // Default attributes
        defaults: function () {
            return {
                Id: 0,
                
                Title: '',
                Description: '',
                CodeGiven: '',
                MaxTimeSpent: 0,
            };
        }
    });

    /**
     * Assignment Collection
     */
    AssignmentCollection = Backbone.Collection.extend({

        // Reference to this collection's model.
        model: AssignmentModel
    });
});