export class IdVM {
    id: string;
}

export class EnumVM {
    index: number;
    name: string;
}

export class ListInterfaceVM {
    id: string;
    name: string;
    description: string;
    lastModified: Date;
    locked: boolean;
    archived: Date;
    historyCount: number;
    propertiesCount: number;
    objectsCount: number;
}

export class EditInterfaceVM {
    id: string;
    name: string;
    description: string;
    properties: PropertyVM[];
    includingIds: string[];
}

export class InterfaceVM {
    id: string;
    name: string;
    locked: boolean;
    archived: Date;
    description: string;
    properties: PropertyVM[];
    history: HistoryVM[];
}

export class PropertyVM {
    name: string;
    description: string;
    type: number;
    required: boolean
}

export class HistoryVM {
    id: string;
    timestamp: Date;
    type: number;
    entityId: string;
    changes: string;
}