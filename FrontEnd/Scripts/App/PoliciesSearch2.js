/// <reference path="../typings/knockout/knockout.d.ts" />
var PoliciesSearch2 = (function () {
    function PoliciesSearch2() {
        this.policies = ko.observableArray();
        this.policyIdFilter = ko.observable(null);
        this.amountInsuredFilter = ko.observable(null);
        this.emailFilter = ko.observable(null);
        this.inceptionDateFilter = ko.observable(null);
        this.installmentPaymentFilterValues = ko.observableArray([true, false]);
        this.totalRecords = ko.observable(null);
        this.totalPages = ko.observable(null);
        this.actualPage = ko.observable(1);
        this.rowsPerPageValues = ko.observableArray([10, 20, 30]);
        this.rowsPerPage = ko.observable(10);
        this.installmentPaymentFilter = ko.observable(null);
    }
    PoliciesSearch2.prototype.applyFilter = function () {
        this.policies.removeAll();
        this.QueryData();
    };
    PoliciesSearch2.prototype.QueryData = function () {
        var p = new PolicyDto();
        p.amountInsured = 100;
        p.email = 'aaa@gmail.com';
        p.id = '0eba1179-6155-41b5-bdd8-f80e1ac94cab';
        p.inceptionDate = new Date(2018, 12, 1);
        p.installmentPayment = false;
        this.policies.push(p);
    };
    return PoliciesSearch2;
}());
var ps = new PoliciesSearch2();
ko.applyBindings(ps);
//# sourceMappingURL=PoliciesSearch2.js.map