import {gql} from '@apollo/client';
import {USER_FRAGMENT} from '../users/users.fragments';
import {EDUCATIONAL_YEAR_FRAGMENT} from '../educationalYears/educationalYears.fragments';

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
