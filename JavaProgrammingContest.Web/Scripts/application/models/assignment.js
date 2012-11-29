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
                
                Title: '',
                Description: '',
                
                CodeGiven: '',
                
                TargetSolveTime: 0.0,
                TargetCompileTime: 0.0,
                
                Solved: false
            };
        }//,

        // Initialize the model
        //initialize: function () {
            
        //}
    });

    /**
     * Assignment Collection
     */
    AssignmentCollection = Backbone.Collection.extend({

        // Reference to this collection's model.
        model: AssignmentModel,

        initialize: function() {
            /*console.log(this.fetch({url: '/api/Assignments', success: function() {
                console.log(this);
            }
            }), this.length);*/
            console.log(this.length);
            //console.log(Backbone.sync('read', this, { url: '/api/assignments' }));

        },

        // Todos are sorted by their original insertion order.
        //comparator: function (assignment) {
        //    return assignment.get('sort');
        //}
    });
});