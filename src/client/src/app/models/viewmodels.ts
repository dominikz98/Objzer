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

export class EditInterfaceVM {
    id: string;
    name: string;
    description: string;
    properties: AddPropertyVM[];
    includingIds: string[];
}

export class InterfaceVM {
    id: string;
    name: string;
    description: string;
    history: HistoryVM[];
    properties: PropertyVM[];
    includings: ReferenceVM[];
}

export class AddPropertyVM {
    name: string;
    description: string;
    type: number;
    required: boolean
}

export class PropertyVM {
    id: string;
    name: string;
    description: string;
    type: number;
    required: boolean;
    history: HistoryVM[];
}

export class HistoryVM {
    id: string;
    timestamp: Date;
    type: number;
    entityId: string;
    changes: string;
}

export class ReferenceVM {
    id: string;
    name: string;
}