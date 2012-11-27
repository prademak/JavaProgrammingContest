var AssignmentModel, AssignmentCollection;

$(window).ready(function () {
    /**
     * Assignment Model
     */
    AssignmentModel = Backbone.Model.extend({

        // Default attributes
        defaults: function () {
            return {
                Id: 0,
                Code: '',
                Time: 0.0,
                CompileTime: 0.0
            };
        },

        // Initialize the model
        initialize: function () {
            
        }
    });

    /**
     * Assignment Collection
     */
    AssignmentCollection = Backbone.Collection.extend({

        // Reference to this collection's model.
        model: AssignmentModel,

        initialize: function() {
            this.fetch({url: '/api/Assignments'});
        },

        // Todos are sorted by their original insertion order.
        comparator: function (assignment) {
            return assignment.get('sort');
        }
    });
});