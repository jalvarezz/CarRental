(function (cr) {
    var ReserveCarViewModel = function () {
        var self = this;

        var initialState = 'reserve';

        self.viewModelHelper = new CarRental.viewModelHelper();
        self.viewMode = ko.observable(); //reserve, carlist, success
        self.reservationModel = ko.observable();
        self.cars = ko.observableArray();
        self.reservationNumber = ko.observable();

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
                                           { pickupDate: model.PickupDate(), returnDate: model.ReturnDate() },
                                           function (result) {
                                               ko.mapping.fromJS(result, {}, self.cars);
                                               self.viewMode('carlist');
                                           });
            } else {
                self.viewModelHelper.modelErrors(error());
            }
        }

        self.selectCar = function (car) {
            var model = {
                PickupDate: self.reservationModel().PickupDate(),
                ReturnDate: self.reservationModel().ReturnDate,
                Car: car.CarId()
            };

            self.viewModelHelper.apiPost('api/reservation/reservecar', model,
                function (result) {
                    self.reservationNumber(result.ReservationId);
                    self.viewMode('success');
                });
        }

        self.reservationDates = function () {
            self.cars([]);
            self.viewMode('reserve');
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

        self.initialize();
    }

    cr.ReserveCarViewModel = ReserveCarViewModel;
}(window.CarRental));