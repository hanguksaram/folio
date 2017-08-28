var Core = {};

/// <summary>Load JSON by AJAX</summary>
/// <param name="url" type="String">URL for load.</param>
/// <param name="successCallback" type="Function">Success function (optional).</param>
/// <param name="data" type="Object">POST data (optional).</param>
/// <param name="errorCallback" type="Function">Error function (optional).</param>
Core.ajax = function (url, successCallback, data, errorCallback, btn, modal) {
    if (!modal) {
        modal = null;
    }
    if (btn) {
        if (btn.text() == "loading...") {
            return;
        }

        btn.button('loading');        
        modal = modal == null ? btn.parent().parent().parent().find('.modal-content') : modal.find('.modal-content');
        if (modal.length > 0) {
            modal.mask2("");
        } else {
            
        }
    }
    
    jQuery.ajax({
        cache: false,        
        contentType: 'application/json',
        dataType: 'json',
        data: JSON.stringify({ data: data }),
        type: 'POST',
        url: url,
        success: function (resp) {
            if (btn) {
                btn.button('reset');
            }
            if (modal != null) {
                modal.unmask();
            }
            if (resp.success) {
                if (!successCallback)
                    return;
                successCallback(resp.response);
            } else {
                if (!errorCallback) {
                    Core.error(resp.response);
                } else {
                    errorCallback(resp.response);
                }
            }
        },
        error: function (res) {
            if (btn) {
                btn.button('reset');
            }
            if (modal != null) {
                modal.unmask();
            }
            Core.error("Error on sending AJAX query");
        }
    }
    );
};

Core.loadFunctions = [];

/// <summary>Exec function on document load</summary>
/// <param name="func" type="Function">Function for load.</param>
Core.load = function (func) {
    Core.loadFunctions.push(func);
};

/// <summary>Display error message</summary>
/// <param name="message" type="String">Message for display.</param>
Core.error = function (message) {
    $.growl({
        message: message,
        type: 'danger',
        title: 'Error',
        delay: 4000,
        pause_on_mouseover: true
    });
};

/// <summary>Display error message</summary>
/// <param name="message" type="String">Message for display.</param>
Core.error = function (message, duration) {
    $.growl({
        message: message,
        type: 'danger',
        title: 'Error',
        delay: duration,
        pause_on_mouseover: true,
       
    });
};

/// <summary>Display notify message</summary>
/// <param name="message" type="String">Message for display.</param>
Core.notify = function (message) {
    $.growl({
        message: message,
        type: 'success',
        title: 'Velvetech ERP',
        delay: 4000,
        pause_on_mouseover: true
    });
};

/// <summary>Display notify message</summary>
/// <param name="message" type="String">Message for display.</param>
Core.notify = function (message, duration) {
    $.growl({
        message: message,
        type: 'success',
        title: 'Velvetech ERP',
        delay: duration,
        pause_on_mouseover: true
    });
};
/// <summary>Save log</summary>
/// <param name="message" type="String">Message for log.</param>
Core.log = function(message) {
    if (console) {
        console.log("LOG:", message);
    }
};

Core.maskPlugin = function() {
    var maskInputs = $('input[data-mask]');
    for (var m = 0; m < maskInputs.length; m++) {
        var input = $(maskInputs[m]);
        var type = input.attr('data-mask');
        if (type == "phone") {
            input.mask("(999) 999-9999");
        }
        if (type == "zip") {
            input.mask('99999?-9999');
        }
        if (type == "phone-ext") {
            input.mask('(999) 999-9999? ext: 99999');
        }
        if (type == "credit-card-number") {
            input.mask('9999-9999-9999-9?999', { placeholder: " " });
        }
        if (type == "date-only") {
            input.mask('99/99/9999');
        }
        if (type == "numeric-field") {
            input.filter_input({ regex: '\\d', live: true });
        }
        if (type == "positive-float-field") {
            input.filter_input({ regex: /[\d\.]/, live: true });
        }
        if (type == "money") {
            input.maskMoney();
        }
    }
};

$(document).ready(function () {

    $.ajaxSetup({ cache: false });

        for (var i = 0; i < Core.loadFunctions.length; i++) {
            try {
                Core.loadFunctions[i]();
            } catch (err) {
                Core.log(err);
            }
        }

        //submit modals on enter
        $(document).keypress(function (event) {
            var keycode = (event.keyCode ? event.keyCode : event.which);
            if (keycode == '13') {
                //get all modals
                var modals = $('.modal');
                for (var m = 0; m < modals.length; m++) {
                    var modal = $(modals[m]);
                    //search opened modal
                    if (modal.css('display') == 'block') {
                        //search primary button
                        var btns = modal.find('.btn-primary');
                        if (btns && btns.length > 0) {
                            //debugger;
                            //find active- visible button;
                            for (var b = 0; b < btns.length; b++) {
                                var btn = $(btns[b]);
                                if (btn.css('display') != 'none') {
                                    //click on first visible button                                                                        
                                    btn.click();                                    
                                    return;
                                }
                            }                                                
                        }
                    }
                }
            
            }
        });
});



(function () {
    (function ($) {
        return $.fn.fixedHeader = function (options) {
            var config;
            config = {
                topOffset: 40,
                bgColor: "#EEEEEE"
            };
            if (options) {
                $.extend(config, options);
            }
            return this.each(function () {
                var $head, $win, headTop, isFixed, o, processScroll, ww;
                processScroll = function () {
                    var headTop, i, isFixed, scrollTop, t;
                    if (!o.is(":visible")) {
                        return;
                    }
                    i = void 0;
                    scrollTop = $win.scrollTop();
                    t = $head.length && $head.offset().top - config.topOffset;
                    if (!isFixed && headTop !== t) {
                        headTop = t;
                    }
                    if (scrollTop >= headTop && !isFixed) {
                        isFixed = 1;
                    } else {
                        if (scrollTop <= headTop && isFixed) {
                            isFixed = 0;
                        }
                    }
                    if (isFixed) {
                        return $("thead.header-copy", o).removeClass("hide");
                    } else {
                        return $("thead.header-copy", o).addClass("hide");
                    }
                };
                o = $(this);
                $win = $(window);
                $head = $("thead.header", o);
                isFixed = 0;
                headTop = $head.length && $head.offset().top - config.topOffset;
                $win.on("scroll", processScroll);
                $head.on("click", function () {
                    if (!isFixed) {
                        return setTimeout((function () {
                            return $win.scrollTop($win.scrollTop() - 47);
                        }), 10);
                    }
                });
                $head.clone().removeClass("header").addClass("header-copy header-fixed").appendTo(o);
                ww = [];
                o.find("thead.header > tr:first > th").each(function (i, h) {
                    return ww.push($(h).width());
                });
                $.each(ww, function (i, w) {
                    return o.find("thead.header > tr > th:eq(" + i + "), thead.header-copy > tr > th:eq(" + i + ")").css({
                        width: w
                    });
                });
                o.find("thead.header-copy").css({
                    margin: "0 auto",
                    width: o.width(),
                    "background-color": config.bgColor
                });
                return processScroll();
            });
        };
    })(jQuery);

}).call(this);
