import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiClient } from './apiclient';
import { EnumVM } from './viewmodels';


@Injectable({
    providedIn: 'root'
})
export class MasterDataEndpoints {

    constructor(private client: ApiClient) { }

    getPropertyTypes(): Observable<EnumVM[]> {
        return this.client.get<EnumVM[]>('masterdata/property/types');
    }
}
