function ContestEditor(editorElem) {
    // Initialize assignment list
    // @TODO stub.

    // Initialize editor
    this.codeMirror = CodeMirror(editorElem, ContestEditor.defaultEditorOptions);

    // Load first assignment
    //this._retrieveAssignmentList();
    this.loadNextAssignment();
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

ContestEditor.prototype.setEditorOptions = function(options) {
    
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
});