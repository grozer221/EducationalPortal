import {gql} from '@apollo/client';
import {USER_FRAGMENT} from '../users/users.fragments';
import {SUBJECT_POST_FRAGMENT} from '../subjectPosts/subjectPosts.fragments';

export const HOMEWORK_FRAGMENT = gql`
    ${USER_FRAGMENT}
    ${SUBJECT_POST_FRAGMENT}
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
            ...SubjectPostFragment
        }
        createdAt
        updatedAt
    }
`;
