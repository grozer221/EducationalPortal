import {File} from "../files/file.types";

export type Backup = {
    id: string,
    file?: File,
    createdAt: string,
    updatedAt: string,
}
