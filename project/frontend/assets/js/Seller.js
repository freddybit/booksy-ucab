import { th } from "vuetify/locale";

export class Seller {
    constructor(_email, _firstName, _lastName, _age, _password, _bankName, _id, _phoneNumber,  _catalog, _ratings,  _salesHistory ) {
        this._id = _id;
        this._email = _email;
        this._firstName = _firstName;
        this._lastName = _lastName;
        this._age = _age;
        this._password = _password;
        this._bankName = _bankName;
        this._phoneNumber = _phoneNumber;
        this._catalog = _catalog;
        this._ratings = _ratings;
        this._salesHistory = _salesHistory;
    }
}