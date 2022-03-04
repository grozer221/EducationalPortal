import {gql} from '@apollo/client';
import {USER_FRAGMENT} from '../users/users.fragments';
import {EDUCATIONAL_YEAR_FRAGMENT} from '../educationalYears/educationalYears.fragments';
import {SUBJECT_POST_FRAGMENT} from '../subjectPosts/subjectPosts.fragments';

export const SUBJECT_FRAGMENT = gql`
    ${USER_FRAGMENT}
    ${EDUCATIONAL_YEAR_FRAGMENT}
    fragment SubjectFragment on SubjectType {
        id
        name
        link
        teacherId
        teacher {
            ...UserFragment
        }
        educationalYearId
        educationalYear {
            ...EducationalYearFragment
        }
        createdAt
        updatedAt
    }
`;

export const SUBJECT_WITH_POSTS_FRAGMENT = gql`
    ${SUBJECT_FRAGMENT}
    ${SUBJECT_POST_FRAGMENT}
    fragment SubjectWithPostsFragment on SubjectType {
        ...SubjectFragment,
        posts(page: $postsPage) {
            entities {
                ...SubjectPostFragment
            }
            total
        }
    }
`;
