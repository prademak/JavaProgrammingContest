$(window).ready(function () {
    /**
     * Score Model
     */
    var ScoreModel = Backbone.Model.extend({

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
     * Score Collection
     */
    var ScoreCollection = Backbone.Collection.extend({

        // Reference to this collection's model.
        model: ScoreModel,

        // Todos are sorted by their original insertion order.
        comparator: function (assignment) {
            return assignment.get('sort');
        }
    });
});