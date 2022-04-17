export class ListInterfaceVM {
    id: string;
    name: string;
    description: string;
    lastModified: Date;
    historyCount: number;
    propertiesCount: number;
    objectsCount: number;
}

export class EnumVM {
    index: number;
    name: string;
}

export class AddInterfaceVM {
    name: string;
    description: string;
    properties: AddPropertyVM[];
    implementationIds: string[];
}

export class AddPropertyVM {
    name: string;
    description: string;
    type: number;
    required: boolean
}