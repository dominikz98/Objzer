import { FormControl, FormGroup, Validators } from "@angular/forms";
import { AddPropertyVM } from "./viewmodels";

export class PropertyModel {
    public form: FormGroup;
    public value: AddPropertyVM;

    constructor(value?: AddPropertyVM) {
        this.value = value;
        if (this.value == null) {
            this.value = new AddPropertyVM();
        }
        this.value.required = true;

        this.form = new FormGroup({
            name: new FormControl(this.value.name, [
                Validators.required,
                Validators.minLength(1)
            ]),
            description: new FormControl(this.value.description),
            type: new FormControl(this.value.type, [
                Validators.required,
                Validators.min(0)
            ]),
            required: new FormControl(this.value.required)
        });
    }

    public fillUp() {
        this.value.name = this.form.value.name;
        this.value.description = this.form.value.description;
        this.value.type = this.form.value.type;
        this.value.required = this.form.value.required;
    }
}
