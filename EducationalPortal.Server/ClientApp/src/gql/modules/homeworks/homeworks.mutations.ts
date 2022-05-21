import {gql} from '@apollo/client';
import {HOMEWORK_FRAGMENT} from './homeworks.fragments';
import {Homework, HomeworkStatus} from './homework.types';

export type CreateHomeworkData = { createHomework: Homework }

export type CreateHomeworkVars = { createHomeworkInputType: createHomeworkInputType }
export type createHomeworkInputType = {
    text: string,
    subjectPostId: string,
    files: File[],
}

export const CREATE_HOMEWORK_MUTATION = gql`
    ${HOMEWORK_FRAGMENT}
    mutation CreateHomework($createHomeworkInputType: CreateHomeworkInputType!) {
        createHomework(createHomeworkInputType: $createHomeworkInputType) {
            ...HomeworkFragment
        }
    }
`;

export type UpdateHomeworkData = { updateHomework: Homework }

export type UpdateHomeworkVars = { updateHomeworkInputType: updateHomeworkInputType }
export type updateHomeworkInputType = { id: string, mark: string, reviewResult: string, status: HomeworkStatus }

export const UPDATE_HOMEWORK_MUTATION = gql`
    ${HOMEWORK_FRAGMENT}
    mutation UpdateHomework($updateHomeworkInputType: UpdateHomeworkInputType!) {
        updateHomework(updateHomeworkInputType: $updateHomeworkInputType) {
            ...HomeworkFragment
        }
    }

`;
//
// export type RemoveSubjectData = { removeSubject: boolean }
// export type RemoveSubjectVars = { id: string }
//
// export const REMOVE_SUBJECT_MUTATION = gql`
//     mutation RemoveSubject($id: ID!) {
//         removeSubject(id: $id)
//     }
// `;
