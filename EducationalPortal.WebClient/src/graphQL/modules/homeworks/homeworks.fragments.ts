import {gql} from '@apollo/client';
import {USER_FRAGMENT} from '../users/users.fragments';
import {FILE_FRAGMENT} from "../files/files.fragments";

// @ts-ignore
export const HOMEWORK_FRAGMENT = gql`
    ${USER_FRAGMENT}
    ${FILE_FRAGMENT}
    fragment HomeworkFragment on HomeworkType {
        id
        text
        mark
        reviewResult
        status
        studentId
        student {
            ...UserFragment
        }
        subjectPostId
        subjectPost {
            id
            title
        }
        files @include(if: $withFiles) {
            ...FileFragment
        }
        createdAt
        updatedAt
    }
`;
