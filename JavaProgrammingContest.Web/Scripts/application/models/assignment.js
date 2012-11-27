$(window).ready(function () {
    /**
     * Assignment Model
     */
    var AssignmentModel = Backbone.Model.extend({

        // Default attributes
        defaults: function () {
            return {
                id: 0
            };
        },

        // 
        initialize: function () {
            if (!this.get("title")) {
                this.set({ "title": this.defaults.title });
            }
        }
    });

    /**
     * Assignment Collection
     */
    var AssignmentCollection = Backbone.Collection.extend({

        // Reference to this collection's model.
        model: AssignmentModel,

        // Todos are sorted by their original insertion order.
        comparator: function (assignment) {
            return assignment.get('sort');
        }
    });
});