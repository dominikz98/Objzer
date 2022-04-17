import { FormArray, FormControl, FormGroup } from "@angular/forms";

export class InterfaceModel {
    public form: FormGroup;

    constructor() {
        this.form = new FormGroup({
            name: new FormControl(''),
            description: new FormControl(''),
            properties: new FormArray([])
        });
    }
}
