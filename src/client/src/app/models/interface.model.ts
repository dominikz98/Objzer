import { FormControl, FormGroup, Validators } from "@angular/forms";
import { EditInterfaceVM } from "./viewmodels";

export class InterfaceModel {
    public form: FormGroup;
    public value: EditInterfaceVM;

    constructor(value?: EditInterfaceVM) {
        this.value = value;

        if (this.value == null) {
            this.value = new EditInterfaceVM();
        }

        if (this.value.properties == null) {
            this.value.properties = [];
        }

        if (this.value.includingIds == null) {
            this.value.includingIds = [];
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
