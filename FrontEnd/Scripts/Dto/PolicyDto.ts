class PolicyDto {
    public id: string;
    public amountInsured: number;
    public email: string;
    public inceptionDate: Date;
    public installmentPayment: boolean;

    public InstallmentPaymentToString(): string {
        if (this.installmentPayment == true) {
            return 'Yes';
        } else {
            return 'No';
        }
    }
}