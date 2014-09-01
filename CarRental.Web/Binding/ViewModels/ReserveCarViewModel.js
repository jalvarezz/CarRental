(function (cr) {
    var ReserveCarViewModel = function () {
        var self = this;

        var initialState = 'reserve';

        self.viewModelHelper = new CarRental.viewModelHelper();
        self.viewMode = ko.observable(); //reserve, carlist, success
        self.reservationModel = ko.observable();

        self.initialize = function () {
            self.reservationModel(new CarRental.ReserveCarModel());
            self.viewMode(initialState);
        };

        self.availableCars = function (model) {
            var errors = ko.validation.group(model, { deep: true });
            self.viewModelHelper.modelIsValid(model.isValid());

            if (errors().length == 0) {
                /* api/reservation/availableCars */
                self.viewModelHelper.apiGet('api/reservation/availableCars',
                                           { pickupDate: model.pickupDate(), returnDate: model.returnDate() },
                                           function (result) {

                                           });
            } else {
                self.viewModelHelper.modelErrors(error());
            }
        }

        self.viewMode.subscribe(function () {
            self.viewModelHelper.pushUrlState(self.viewMode(), null, null, 'customer/reserve');
            initialState = self.viewModelHelper.handleUrlState(initialState);
        });

        if (Modernizr.history) {
            window.onpopstate = function (arg) {
                if (arg.state != null) {
                    self.viewModelHelper.statePopped = true;
                    self.viewMode(arg.state.Code);
                }
            }
        }
    }

    cr.ReserveCarViewModel = ReserveCarViewModel;
}(window.CarRental));