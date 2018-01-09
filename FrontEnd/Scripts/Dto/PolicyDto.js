var PolicyDto = (function () {
    function PolicyDto() {
    }
    PolicyDto.prototype.InstallmentPaymentToString = function () {
        if (this.installmentPayment == true) {
            return 'Yes';
        }
        else {
            return 'No';
        }
    };
    return PolicyDto;
}());
//# sourceMappingURL=PolicyDto.js.map