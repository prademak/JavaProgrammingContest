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
        
        urlRoot: '/api/progress',
        
        
    });
});

if (!API) var API = {};
API.Progress = {
    start: function (id, success) {
        return APIHelper.put('progress/' + id, { Id: id, StartTime: 0 }, success);
    },

    stop: function (id, success) {
        return APIHelper.delete('progress/' + id, { Id: id }, success);
    },

    inProgress: function (id, success) {
        return APIHelper.get('progress/' + id, { assignmentId: id }, success);
    }
};