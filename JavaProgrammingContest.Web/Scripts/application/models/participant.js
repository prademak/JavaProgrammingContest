$(window).ready(function () {
    /**
     * Participant Model
     */
    var ParticipantModel = Backbone.Model.extend({

        // Default attributes
        defaults: function () {
            return {
                id: 0,
                firstname: "John",
                insert: "",
                lastname: "Doe",
                password: "",
                email: "",
                interested: true
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
     * Participant Collection
     */
    var ParticipantScoreList = Backbone.Collection.extend({

        // Reference to this collection's model.
        model: ParticipantModel,

        // Todos are sorted by their original insertion order.
        comparator: function (participant) {
            return participant.get('score');
        }
    });
});