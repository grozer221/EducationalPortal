import {gql} from '@apollo/client';
import {BACKUP_FRAGMENT} from "./backups.fragments";
import {Backup} from "./backup.types";

export type CreateBackupData = { createBackup: Backup }

export type CreateBackupVars = {}

export const CREATE_BACKUP_MUTATION = gql`
    ${BACKUP_FRAGMENT}
    mutation CreateBackup {
        createBackup{
            ...BackupFragment
        }
    }

`;

export type RestoreBackupData = { restoreBackup: Backup }
export type RestoreBackupVars = { id: string }

export const RESTORE_BACKUP_MUTATION = gql`
    ${BACKUP_FRAGMENT}
    mutation RestoreBackup($id: Guid!) {
        restoreBackup(id: $id) {
            ...BackupFragment
        }
    }
`;

export type RemoveBackupData = { removeBackup: boolean }
export type RemoveBackupVars = { id: string }

export const REMOVE_BACKUP_MUTATION = gql`
    mutation RemoveBackup($id: Guid!) {
        removeBackup(id: $id)
    }
`;