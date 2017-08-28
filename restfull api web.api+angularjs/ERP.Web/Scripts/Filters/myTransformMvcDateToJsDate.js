 function myTransformMvcDateToJsDate() {

    var subStringStartPosition = 6;
    var notation = 10;

    return function(mvcDate) {
        return new Date(parseInt(mvcDate.substr(subStringStartPosition), notation));
    }

}