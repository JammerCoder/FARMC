var ObjectState = {
    Unchanged: 0,
    Added: 1,
    Modified: 2,
    Deleted: 3
};

var fishCatchMapping = {
    'FishCatches': {
        key: function(fishCatch) {
            return ko.utils.unwrapObservable(fishCatch.FishCatchId);
        },
        create: function(options) {
            return new FishCatchViewModel(options.data);
        }
    }
};

FishCatchViewModel = function(data) {
    var self = this;
    ko.mapping.fromJS(data, fishCatchMapping, self);

    self.flagFishCatchAsEdited = function () {
        if (self.ObjectState() != ObjectState.Added) {
            self.ObjectState(ObjectState.Modified);
        }

        return true;
    },

    self.SubTotal = ko.computed(function() {
        return (self.Qty() * self.UnitPrice()).toFixed(2);
    });
};

FisherfolkViewModel = function(data) {
    var self = this;
    ko.mapping.fromJS(data, fishCatchMapping, self);

    self.save = function() {
            $.ajax({
                url: "/Fisherfolks/Save/",
                type: "POST",
                data: ko.toJSON(self),
                contentType: "application/json",
                success: function(data) {
                    if (data.fisherfolkViewModel != null)
                        ko.mapping.fromJS(data.fisherfolkViewModel, {}, self);

                    if (data.newLocation != null)
                        window.location = data.newLocation;
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    if (XMLHttpRequest.status == 400) {
                        $('#MessageToUser').text(XMLHttpRequest.responseText);
                    }
                    else {
                        $('#MessageToUser').text('The Server had an error.');
                    }
                }
            });
    },

    self.flagfisherfolkAsEdited = function () {
        if (self.ObjectState() != ObjectState.Added) {
            self.ObjectState(ObjectState.Modified);
        }

        return true;
    },
        
    self.addFishCatch = function () {
        var fishCatch = new FishCatchViewModel({ FishCatchId: 0, FishName: "", Qty: 1, UnitPrice: 1, ObjectState: ObjectState.Added });
        self.FishCatches.push(fishCatch);
    },

    self.Total = ko.computed(function () {
        var total = 0;
        ko.utils.arrayForEach(self.FishCatches(), function (fishCatch) {
            total += parseFloat(fishCatch.SubTotal());
        });

        return total.toFixed(2);
    }),

    self.deleteFishCatch = function (fishCatch) {
        self.FishCatches.remove(this);

        if (fishCatch.FishCatchId() > 0 && self.FishCatchesToDelete.indexOf(fishCatch.FishCatchId()) == -1)
            self.FishCatchesToDelete.push(fishCatch.FishCatchId());
    };  
};

$("form").validate({
    submitHandler: function() {
        fisherfolkViewModel.save();
    },

    rules: {
        FisherId: {
            //required: true,
            maxlength: 15
        },
        LastName: {
            required: true,
            maxlength: 30,
            alphaonly: true
        },
        FishName: {
            required: true,
            maxlength: 20,
            alphaonly: true
        },
        Qty: {
            required: true,
            number: true,
            range: [1, 1000000]
        },
        UnitPrice: {
            required: true,
            number: true,
            range: [1, 1000000]
        }
    },

    messages: {
        FisherId: {
            //required: "Cannot process transaction without Fisherfolk ID.",
            maxlength: "Fisherfolk ID must not exceed 15 characters long."
        },
        LastName: {
            required: "Cannot process transaction without Fisherfolk Name.",
            maxlength: "Fisherfolk Name must not exceed 30 characters long."
        },
        FishName: {
            required: "Cannot process transaction without Fish Name.",
            maxlength: "Fish Name must not exceed 20 characters long."
        }
    },

    tooltip_options: {
        FisherID: {
            placement: 'right'
        },
        LastName: {
            placement: 'right'
        }
    }
});

$.validator.addMethod("alphaonly",
    function(value) {
        return /^[A-Za-z]+$/.test(value);
    }
);