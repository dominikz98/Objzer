import { FormArray, FormControl, FormGroup, Validators } from "@angular/forms";
import { AddPropertyVM } from "../endpoints/viewmodels";

export class PropertyModel {
    public form: FormGroup;

    constructor(property: AddPropertyVM) {
        this.form = new FormGroup({
            name: new FormControl(property.name, [
                Validators.required,
                Validators.minLength(1)
            ]),
            description: new FormControl(property.description),
            type: new FormControl(property.type, [
                Validators.required,
                Validators.min(0)
            ]),
            required: new FormControl(property.required, [
                Validators.required
            ])
        });
    }
}
