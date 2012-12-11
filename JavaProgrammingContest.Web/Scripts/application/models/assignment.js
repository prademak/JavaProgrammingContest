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
        },

        
        // Initialize the model
        //initialize: function () {
            
        //},

        solve: function (code) {
            
        }
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