import {gql} from '@apollo/client';

export const EDUCATIONAL_YEAR_FRAGMENT = gql`
    fragment EducationalYearFragment on EducationalYearType {
        id
        name
        dateStart
        dateEnd
        isCurrent
        createdAt
        updatedAt
    }
`;
