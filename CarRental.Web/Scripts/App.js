
window.CarRental = {};

(function (cr) {
    var initialId;
    cr.initialId = initialId;
}(window.CarRental));

(function (cr) {
    var initialState;
    cr.initialState = initialState;
}(window.CarRental));

(function (cr) {
    var rootPath;
    cr.rootPath = rootPath;
}(window.CarRental));

(function (cr) {
    var mustEqual = function (val, other) {
        return val == other;
    }
    cr.mustEqual = mustEqual;
}(window.CarRental));

(function (cr) {
    var viewModelHelper = function () {
        var self = this;

        self.modelIsValid = ko.observable(true);
        self.modelErrors = ko.observableArray();
        self.isLoading = ko.observable(false);

        self.statePopped = false;
        self.stateInfo = {};

        self.apiGet = function (uri, data, success, failure, always) {
            self.isLoading(true);
            self.modelIsValid(true);
            $.get(CarRental.rootPath + uri, data)
                .done(success)
                .fail(function (result) {
                    if (failure == null) {
                        if (result.status != 400)
                            self.modelErrors([result.status + ':' + result.statusText + ' - ' + result.responseText]);
                        else
                            self.modelErrors(JSON.parse(result.responseText));
                        self.modelIsValid(false);
                    } else {
                        failure(result);
                    }
                })
                .always(function () {
                    if (always == null)
                        self.isLoading(false);
                    else
                        always();
                });
        }

        self.apiPost = function (uri, data, success, failure, always) {
            self.isLoading(true);
            self.modelIsValid(true);
            $.post(CarRental.rootPath + uri, data)
                .done(success)
                .fail(function (result) {
                    if (failure == null) {
                        if (result.status != 400)
                            self.modelErrors([result.status + ':' + result.statusText + ' - ' + result.responseText]);
                        else
                            self.modelErrors(JSON.parse(result.responseText));
                        self.modelIsValid(false);
                    } else {
                        failure(result);
                    }
                })
                .always(function () {
                    if (always == null) {
                        self.isLoading(false);
                    }
                    else {
                        always();
                    }
                });
        }

        self.pushUrlState = function (code, title, id, url) {
            self.stateInfo = { State: { Code: code, Id: id }, Title: title, Url: CarRental.rootPath + url };
        }

        self.handleUrlState = function (initialState) {
            if (!self.statePopped) {
                if (initialState != '') {
                    history.replaceState(self.stateInfo.State, self.stateInfo.Title, self.stateInfo.Url);

                    initialState = '';
                } else {
                    history.pushState(self.stateInfo.State, self.stateInfo.Title, self.stateInfo.Url);
                }
            }
            else {
                self.statePopped = false;
            }

            return initialState;
        }
    }

    cr.viewModelHelper = viewModelHelper;
}(window.CarRental));

ko.bindingHandlers.datepicker = {
    init: function (element, valueAccessor, allBindingAccessor) {
        //Initialize datepicker with optional options
        var options = allBindingAccessor().datepickerOptions || {};
        $(element).datepicker(options);

        //Handler the field changing
        ko.utils.registerEventHandler(element, "change", function () {
            var observable = valueAccessor();
            var newDate = $(element).datepicker("getDate");
            // newDate format is 2013-01-11T06:11:00.000Z
            observable(moment(newDate).format('MM/DD/YYY'));
        });

        //Handle disposal (if KO removes by the template binding)
        ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
            $(element).datepicker("destroy");
        });
    },
    // get the value from the viewmodel and format it for display
    update: function (element, valueAccessor) {
        var value = ko.utils.unwrapObservable(valueAccessor());
        current = $(element).datepicker("getDate");

        if (value != null) {
            if (value - current !== 0) {
                var date = moment(value);
                $(element).val(date.format("L"));
            }
        }
    }
};

ko.bindingHandlers.loadingWhen = {
    // any ViewModel using this extension needs a property called isLoading
    // the div tag to contain the loaded content uses a data-binding="loadingWhen: isLoading"
    init: function (element) {
        var $element = $(element),
                        currentPosition = $element.css("position"),
                         $loader = $("<div>").addClass("loading-loader").hide();

        //add the loader
        $element.append($loader);

        //make sure that we can absolutely position the loader against the original element
        if (currentPosition == "auto" || currentPosition == "static")
            $element.css("position", "relative");

        //center the loader
        $loader.css({
            position: "absolute",
            top: "50%",
            left: "50%",
            "margin-left": -($loader.width() / 2) + "px",
            "margin-top": -($loader.height() / 2) + "px"
        });
    },
    update: function (element, valueAccessor) {
        var isLoading = ko.utils.unwrapObservable(valueAccessor()),
            $element = $(element),
            $childrenToHide = $element.children(":not(div.loading-loader)"),
            $loader = $element.find("div.loading-loader");

        if (isLoading) {
            $childrenToHide.css("visibility", "hidden").attr("disable", "disable");
            $loader.show();
        } else {
            $childrenToHide.css('visibility', 'visible').removeAttr('disabled');
            $loader.hide();
        }
    }
};