import {gql} from '@apollo/client';
import {USER_FRAGMENT} from '../users/users.fragments';
import {HOMEWORK_FRAGMENT} from "../homeworks/homeworks.fragments";
import {FILE_FRAGMENT} from "../files/files.fragments";

export const BACKUP_FRAGMENT = gql`
    ${FILE_FRAGMENT}
    fragment BackupFragment on BackupType {
        id
        file {
            ...FileFragment
        }
        createdAt
        updatedAt
    }
`;
