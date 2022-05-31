import { FormControl, FormGroup, Validators } from "@angular/forms";
import { ObjectVM } from "./viewmodels";

export class ObjectModel {
    public form: FormGroup;
    public value: ObjectVM;

    constructor(value?: ObjectVM) {
        this.value = value;

        if (this.value == null) {
            this.value = new ObjectVM();
        }

        if (this.value.properties == null) {
            this.value.properties = [];
        }

        this.form = new FormGroup({
            name: new FormControl(this.value.name, [
                Validators.required,
                Validators.minLength(1)
            ]),
            description: new FormControl(this.value.description, [
                Validators.required,
                Validators.minLength(1)
            ])
        });
    }

    public fillUp() {
        this.value.name = this.form.value.name;
        this.value.description = this.form.value.description;
    }
}
