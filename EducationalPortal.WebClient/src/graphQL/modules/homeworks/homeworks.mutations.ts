import {gql} from '@apollo/client';
import {HOMEWORK_FRAGMENT} from './homeworks.fragments';
import {Homework, HomeworkStatus} from './homework.types';

export type CreateHomeworkData = { createHomework: Homework }

export type CreateHomeworkVars = { createHomeworkInputType: CreateHomeworkInputType, withFiles: boolean }
export type CreateHomeworkInputType = {
    text: string,
    subjectPostId: string,
    files?: File[] | null,
}

export const CREATE_HOMEWORK_MUTATION = gql`
    ${HOMEWORK_FRAGMENT}
    mutation CreateHomework($createHomeworkInputType: CreateHomeworkInputType!, $withFiles: Boolean!) {
        createHomework(createHomeworkInputType: $createHomeworkInputType) {
            ...HomeworkFragment
        }
    }
`;

export type UpdateHomeworkData = { updateHomework: Homework }

export type UpdateHomeworkVars = { updateHomeworkInputType: updateHomeworkInputType, withFiles: boolean }
export type updateHomeworkInputType = { id: string, mark: string, reviewResult: string, status: HomeworkStatus }

export const UPDATE_HOMEWORK_MUTATION = gql`
    ${HOMEWORK_FRAGMENT}
    mutation UpdateHomework($updateHomeworkInputType: UpdateHomeworkInputType!, $withFiles: Boolean!) {
        updateHomework(updateHomeworkInputType: $updateHomeworkInputType) {
            ...HomeworkFragment
        }
    }

`;
//
// export type RemoveSubjectData = { removeSubject: boolean }
// export type RemoveSubjectVars = { id: string }
//
// export const REMOVE_SUBJECT_MUTATION = graphQL`
//     mutation RemoveSubject($id: ID!) {
//         removeSubject(id: $id)
//     }
// `;
