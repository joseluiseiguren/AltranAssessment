function ValidateUnauthorized(status: number): void {
    if (status == 401) {
        window.location.replace("/users/logout");
    }
}