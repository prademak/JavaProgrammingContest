function ContestEditor(editorElem) {
    // Initialize assignment list
    // @TODO stub.

    // Initialize editor
    this.codeMirror = CodeMirror(editorElem, ContestEditor.defaultEditorOptions);

    // Load first assignment
    this._retrieveAssignmentList();
    //this.loadNextAssignment();
}

// Static Properties
ContestEditor.defaultEditorOptions = {
    mode: "text/x-java",
    theme: "eclipse",
    value: "// No assignment loaded yet.",
    
    lineNumbers: true,
    lineWrapping: false,
    gutter: true,
    fixedGutter: true,

    tabSize: 4,
    indentUnit: 4,
    indentWithTabs: true,
    smartIndent: true,
    
    electricChars: true,
    matchBrackets: true
};

// Public Properties
ContestEditor.prototype.codeMirror = null;
ContestEditor.prototype.currentAssignment = null;
ContestEditor.prototype.assignmentList = null;
ContestEditor.prototype.assignmentList = [{title: "Dit is een test", content: "import System.*;\n\ninterface lol {\n\t\n}\n"}];

// Public Methods
ContestEditor.prototype.setEditorContent = function(content) {
    return this.codeMirror.setValue(content);
};

ContestEditor.prototype.setOption = function(option, value) {
    this.codeMirror.setOption(option, value);
};


ContestEditor.prototype.loadAssignment = function(index) {
    if (index == this.currentAssignment) return;

    this.setTitle(this.assignmentList[index].title);
    this.setEditorContent(this.assignmentList[index].content);
};

/**
 * Load the next assignment, if it is applicable
 */
ContestEditor.prototype.loadNextAssignment = function() {
    if (this.currentAssignment == null) this.currentAssignment = 0;
    else if (this.assignmentList.length > this.currentAssignment) this.currentAssignment++;
    else console.warn("No more assignments left, could not load next assignment.");

    this.loadAssignment(this.currentAssignment);
};

// Protected Methods
ContestEditor.prototype._retrieveAssignmentList = function() {
    var ns = this;
    $.ajax({
        url: "/api/assignments/",
        method: "get",
        dataType: 'json',
        
        callback: function (data) {
            ns.assignmentList = data;
            ns.loadAssignment(0);
        },
        
        error: function () {
            // @TODO fancy dis up
            console.warn("Got an error while trying to retrieve the assignment list.");
        }
    });
};

// Initialize the editor environment
$(document).ready(function () {
    window.cedit = new ContestEditor($('#ContestEditor').get(0));

    $('.pane.editor .dropdown-menu a').click(function () {
        window.cedit.setOption('theme', $(this).attr('data-theme'));
    });

    $('#settings-save').click(function() {
        $('#settings-dialog form input, #settings-dialog form select').each(function (key, value) {
            var el = $(value);
            if (el.attr('data-original') != el.val()) {
                console.log('Diffrnce '+el.val());
                
                // Submit
                /*$.ajax({
                    // @TODO Use a real user id
                    url: '/api/settings/01',
                    method: 'get',
                    dataType: 'json',

                    callback: function (data) {
                        $.each(data, function (key, value) {
                            var curEl = $('#settings-dialog form input[name~=' + key + ']');
                            curEl.attr('data-original', value);
                            curEl.value(value);
                        });
                    }
                });*/

                // Set the originals
                el.attr('data-original', el.val());
            }
        });
    });
    
    function populateSettings() {
        // Check if they are already populated
        if ($('#settings-dialog form input:first-child').attr('data-original') == "") {
            // Get settings from REST Service
            $.ajax({
                // @TODO Use a real user id
                url: '/api/Settings/01',
                method: 'get',
                dataType: 'json',

                callback: function(data) {
                    $.each(data, function (key, value) {
                        var curEl = $('#settings-dialog form input[name~=' + key + ']');
                        curEl.attr('data-original', value);
                        curEl.val(value);
                    });
                }
            });
        } else {
            // Populate the original fields from the values
            $('#settings-dialog form input, #settings-dialog form select').each(function(key, value) {
                
            });
        }
    }

    populateSettings();
});